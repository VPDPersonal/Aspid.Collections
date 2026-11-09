import fs from 'node:fs';
import path from 'node:path';
import {fileURLToPath} from 'node:url';
import {changelogPage} from './changelog-page.mjs';

const site = fileURLToPath(new URL('../', import.meta.url));
const repo = path.resolve(site, '..');
const pkg = path.join(repo, 'Aspid.Collections/Packages/tech.aspid.collections');
const manifest = JSON.parse(fs.readFileSync(path.join(pkg, 'package.json'), 'utf8'));
const check = process.argv.includes('--check');
let stale = false;
export const sampleSlug = name => name.replace(/([a-z])([A-Z])/g, '$1-$2').toLowerCase();

function write(file, content) {
  const expected = Buffer.isBuffer(content) ? content : Buffer.from(content);
  if (check) {
    if (!fs.existsSync(file) || !fs.readFileSync(file).equals(expected)) {console.error(`Out of date: ${path.relative(site, file)}`); stale = true;}
  } else {fs.mkdirSync(path.dirname(file), {recursive: true}); fs.writeFileSync(file, expected);}
}

if (process.argv[1] && path.resolve(process.argv[1]) === fileURLToPath(import.meta.url)) {
  for (const sample of manifest.samples) {
    const folder = path.join(pkg, sample.path);
    const slug = sampleSlug(path.basename(folder));
    let text = fs.readFileSync(path.join(folder, 'README.md'), 'utf8');
    text = text.replace(/!\[([^\]]*)\]\(Preview\.png\)/g,
      (_, alt) => `<SamplePreview sample="${slug}" alt={${JSON.stringify(alt)}} />`);
    text = text.replace(/^\[Светлая тема\]\(Preview-light\.png\)\n?/gm, '');
    const title = text.match(/^# (.+)$/m)[1];
    const shortTitle = title.split(' — ')[1] ?? title;
    const label = shortTitle[0].toUpperCase() + shortTitle.slice(1);
    text = text.replace(/^(# .+)$/m, '$1\n\n<SampleAvailability />');
    write(path.join(site, 'i18n/ru/docusaurus-plugin-content-docs-tutorials/current', `${slug}.md`), `---\nid: ${slug}\nslug: /${slug}\ntitle: ${JSON.stringify(title)}\nsidebar_label: ${JSON.stringify(label)}\n---\n\n${text}`);
    write(path.join(site, 'static/img/samples', `${slug}.png`), fs.readFileSync(path.join(folder, 'Preview.png')));
    write(path.join(site, 'static/img/samples', `${slug}-light.png`), fs.readFileSync(path.join(folder, 'Preview-light.png')));
  }
  write(path.join(site, 'changelog/index.md'), changelogPage(fs.readFileSync(path.join(repo, 'CHANGELOG.md'), 'utf8')));
  if (stale) process.exitCode = 1;
  else console.log(`${check ? 'Verified' : 'Synchronized'} ${manifest.samples.length} tutorials and previews, plus changelog.`);
}
