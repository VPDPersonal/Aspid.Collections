import {execFileSync} from 'node:child_process';
import {readFileSync} from 'node:fs';
import venom from './src/prism/venom.js';
import {ACCENT_BOOT_SCRIPT} from './src/accents.js';
import {INTRO_BOOT_SCRIPT} from './src/intro.js';

const REPO = 'https://github.com/VPDPersonal/Aspid.Collections';
const BASE_URL = '/Aspid.Collections/';
const manifest = JSON.parse(readFileSync(new URL('../Aspid.Collections/Packages/tech.aspid.collections/package.json', import.meta.url)));
const tags = execFileSync('git', ['tag', '--sort=-version:refname', '--list', 'upm*/*'], {encoding: 'utf8'}).trim().split(/\s+/);
function versions(channel) {
  const prefix = channel + '/';
  const releases = tags.filter(tag => tag.startsWith(prefix)).map(tag => tag.slice(prefix.length));
  if (channel === (manifest.version.includes('-') ? 'upm-preview' : 'upm') && !releases.includes(manifest.version)) releases.unshift(manifest.version);
  return releases;
}
function section(id) {
  return ['@docusaurus/plugin-content-docs', {id, path: id, routeBasePath: id, breadcrumbs: false,
    sidebarPath: './' + id + '/sidebar.js'}];
}

export default {
  title: 'Aspid.Collections',
  tagline: 'Observable collections for Unity and .NET',
  url: 'https://vpdpersonal.github.io',
  baseUrl: BASE_URL,
  trailingSlash: true,
  customFields: {packageVersion: manifest.version, packageVersions: {upm: versions('upm'), 'upm-preview': versions('upm-preview')}},
  headTags: [
    {tagName: 'script', attributes: {}, innerHTML: ACCENT_BOOT_SCRIPT},
    {tagName: 'script', attributes: {}, innerHTML: INTRO_BOOT_SCRIPT},
    {tagName: 'link', attributes: {rel: 'icon', href: BASE_URL + 'img/logo.png'}},
  ],
  clientModules: ['./src/clientModules/accent.js'],
  onBrokenLinks: 'throw',
  markdown: {hooks: {onBrokenMarkdownLinks: 'throw'}},
  i18n: {defaultLocale: 'en', locales: ['en','ru'], localeConfigs: {en: {label: 'English'}, ru: {label: 'Русский'}}},
  presets: [['classic', {
    docs: {routeBasePath: 'docs', breadcrumbs: false, sidebarPath: './sidebars.js'},
    blog: false,
    theme: {customCss: ['./src/css/fonts.css', './src/css/custom.css', './src/css/accents.css']},
  }]],
  plugins: ['./src/plugins/search/index.js', './src/plugins/fonts/index.js', ...['tutorials','api','changelog'].map(section)],
  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      colorMode: { defaultMode: 'dark', respectPrefersColorScheme: false },
      // Link previews (og:image, twitter:image) for every page.
      image: 'img/logo.png',
      navbar: {
        title: 'Aspid.Collections',
        logo: { alt: 'Aspid.Collections', src: 'img/logo.png', width: 28, height: 28 },
        hideOnScroll: false,
        items: [
          { type: 'docSidebar', sidebarId: 'docs', position: 'left', label: 'Docs' },
          { type: 'docSidebar', docsPluginId: 'tutorials', sidebarId: 'tutorials', position: 'left', label: 'Samples' },
          { type: 'docSidebar', docsPluginId: 'api', sidebarId: 'api', position: 'left', label: 'API' },
          { to: '/changelog', label: 'Changelog', position: 'left' },
          { type: 'localeDropdown', position: 'right', className: 'navbar-locale' },
          { href: REPO, label: 'GitHub', position: 'right', className: 'navbar-github', 'aria-label': 'GitHub' },
        ],
      },
      footer: {
        style: 'dark',
        // One row: the author's contacts. Docs, GitHub, Asset Store and the changelog already live in the sidebar panel.
        links: [
          { label: 'Vladislav Panin', href: 'https://github.com/VPDPersonal' },
          { label: 'LinkedIn', href: 'https://www.linkedin.com/in/vpdaspid/' },
          { label: 'X', href: 'https://x.com/VPDInc' },
          { label: 'vpd.aspid@gmail.com', href: 'mailto:vpd.aspid@gmail.com' },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} Vladislav Panin. MIT License.`,
      },
      prism: {
        theme: venom.light,
        darkTheme: venom.dark,
        additionalLanguages: ['csharp', 'json', 'bash'],
      },
    }),
};

