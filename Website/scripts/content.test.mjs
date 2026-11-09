import assert from 'node:assert/strict';
import {test} from 'node:test';
import fs from 'node:fs';
import path from 'node:path';
import {fileURLToPath} from 'node:url';
import {sampleSlug} from './sync-content.mjs';

const site = fileURLToPath(new URL('../', import.meta.url));
const pkg = path.resolve(site, '../Aspid.Collections/Packages/tech.aspid.collections');
const manifest = JSON.parse(fs.readFileSync(path.join(pkg, 'package.json'), 'utf8'));
const blocks = text => [...text.matchAll(/```(\w+)\n([\s\S]*?)```/g)].map(match => [match[1], match[2]]);

test('every guide has a Russian twin with the same executable code exhibits', () => {
  for (const file of fs.readdirSync(path.join(site, 'docs'))) {
    const source = fs.readFileSync(path.join(site, 'docs', file), 'utf8');
    const translated = fs.readFileSync(path.join(site, 'i18n/ru/docusaurus-plugin-content-docs/current', file), 'utf8');
    assert.deepEqual(blocks(source), blocks(translated), file);
    assert.equal((source.match(/^## /gm) ?? []).length, (translated.match(/^## /gm) ?? []).length, file);
  }
});

test('all package samples have English tutorials with the canonical C# exhibits', () => {
  for (const sample of manifest.samples) {
    const folder = path.join(pkg, sample.path);
    const slug = sampleSlug(path.basename(folder));
    const tutorial = fs.readFileSync(path.join(site, 'tutorials', `${slug}.md`), 'utf8');
    assert.deepEqual(blocks(tutorial), blocks(fs.readFileSync(path.join(folder, 'README.md'), 'utf8')), slug);
    assert.ok(fs.statSync(path.join(folder, 'Preview.png')).size > 0, slug);
    const dark = fs.readFileSync(path.join(folder, 'Preview.png'));
    const light = fs.readFileSync(path.join(folder, 'Preview-light.png'));
    assert.ok(!dark.equals(light), `${slug}: theme previews must be different renders`);
    assert.ok(tutorial.includes(`<SamplePreview sample="${slug}"`), `${slug}: tutorial must follow the site theme`);
  }
});

test('API pages cover all public runtime types and omit internal sync implementations', () => {
  function visit(dir) {
    return fs.readdirSync(dir, {withFileTypes: true}).flatMap(entry => entry.isDirectory()
      ? visit(path.join(dir, entry.name)) : entry.name.endsWith('.cs') ? [path.join(dir, entry.name)] : []);
  }
  for (const file of visit(path.join(pkg, 'Runtime/Observable'))) {
    const source = fs.readFileSync(file, 'utf8');
    const namespace = source.match(/namespace (\S+)/)?.[1];
    for (const match of source.matchAll(/public\s+(?:(?:static|abstract|sealed|readonly)\s+)*(?:class|interface|struct|delegate\s+void)\s+(\w+)(?:<([^>]+)>)?/g)) {
      const arity = match[2] ? `-${match[2].split(',').length}` : '';
      assert.ok(fs.existsSync(path.join(site, 'api', `${namespace}.${match[1]}${arity}.md`)), match[1]);
    }
  }
  assert.ok(!fs.existsSync(path.join(site, 'api/Aspid.Collections.Observable.Synchronizer.ObservableListSync-2.md')));
});
