// @ts-check
import {readFileSync} from 'node:fs';
import {execFileSync} from 'node:child_process';
import venom from './src/prism/venom.js';
import admonitions from 'remark-github-admonitions-to-directives';
import {ACCENT_BOOT_SCRIPT} from './src/accents.js';
import {INTRO_BOOT_SCRIPT} from './src/intro.js';

const manifest = JSON.parse(readFileSync(new URL('../Aspid.Collections/Packages/tech.aspid.collections/package.json', import.meta.url), 'utf8'));
const repo = 'https://github.com/VPDPersonal/Aspid.Collections';
const baseUrl = '/Aspid.Collections/';
let samplesUnreleased = true;
try {
  const released = JSON.parse(execFileSync('git', ['show', `upm/${manifest.version}:package.json`], {encoding: 'utf8', stdio: ['ignore', 'pipe', 'ignore']}));
  samplesUnreleased = JSON.stringify(released.samples) !== JSON.stringify(manifest.samples);
} catch { /* New versions and source archives have no matching published tag yet. */ }
let tags = [];
try {
  tags = execFileSync('git', ['tag', '-l', 'upm/*'], {encoding: 'utf8'}).trim().split('\n')
    .map(tag => tag.replace(/^upm\//, '')).filter(Boolean);
} catch { /* A source archive has no Git metadata. */ }
// This repository publishes all immutable version tags under upm/, including prereleases.
const packageVersions = Object.fromEntries(['upm', 'upm-preview'].map(channel => [channel,
  [...new Set([...tags, manifest.version])].filter(version => version.includes('-') === (channel === 'upm-preview'))
    .sort((a, b) => b.localeCompare(a, 'en', {numeric: true})),
]));
const docsOptions = {
  versions: {current: {label: manifest.version}},
  breadcrumbs: false,
  showLastUpdateTime: false,
  beforeDefaultRemarkPlugins: [admonitions],
  editUrl: `${repo}/edit/main/Website/`,
  editLocalizedFiles: true,
};

/** @type {import('@docusaurus/types').Config} */
export default {
  title: 'Aspid.Collections',
  tagline: 'Observable collections for Unity and .NET',
  url: 'https://vpdpersonal.github.io',
  baseUrl,
  organizationName: 'VPDPersonal',
  projectName: 'Aspid.Collections',
  trailingSlash: false,
  onBrokenLinks: 'throw',
  onBrokenAnchors: 'throw',
  markdown: {hooks: {onBrokenMarkdownLinks: 'throw'}},
  i18n: {defaultLocale: 'en', locales: ['en', 'ru'], localeConfigs: {en: {label: 'English'}, ru: {label: 'Русский'}}},
  headTags: [
    {tagName: 'script', attributes: {}, innerHTML: ACCENT_BOOT_SCRIPT},
    {tagName: 'script', attributes: {}, innerHTML: INTRO_BOOT_SCRIPT},
    {tagName: 'link', attributes: {rel: 'icon', href: `${baseUrl}img/logo.png`}},
  ],
  clientModules: ['./src/clientModules/accent.js'],
  customFields: {packageVersion: manifest.version, packageVersions, samplesUnreleased, framedCaptures: {margin: 0, names: []}},
  presets: [['classic', {
    docs: {path: 'docs', routeBasePath: 'docs', sidebarPath: './sidebars.js', ...docsOptions},
    blog: false,
    theme: {customCss: ['./src/css/fonts.css', './src/css/custom.css', './src/css/accents.css', './src/css/collections.css']},
  }]],
  plugins: [
    './src/plugins/search/index.js', './src/plugins/fonts/index.js',
    ['@docusaurus/plugin-content-docs', {id: 'tutorials', path: 'tutorials', routeBasePath: 'tutorials', sidebarPath: './sidebarsTutorials.js', ...docsOptions}],
    ['@docusaurus/plugin-content-docs', {id: 'api', path: 'api', routeBasePath: 'api', sidebarPath: './sidebarsApi.js', ...docsOptions, editUrl: undefined}],
    ['@docusaurus/plugin-content-docs', {id: 'changelog', path: 'changelog', routeBasePath: 'changelog', sidebarPath: './sidebarsChangelog.js', ...docsOptions, editUrl: `${repo}/edit/main/CHANGELOG.md`}],
  ],
  themeConfig: {
    colorMode: {defaultMode: 'dark', respectPrefersColorScheme: false},
    navbar: {
      title: 'Aspid.Collections', logo: {alt: 'Aspid.Collections', src: 'img/logo.png', width: 28, height: 28},
      items: [
        {type: 'docSidebar', sidebarId: 'docs', position: 'left', label: 'Docs'},
        {type: 'docSidebar', docsPluginId: 'tutorials', sidebarId: 'tutorials', position: 'left', label: 'Samples'},
        {type: 'docSidebar', docsPluginId: 'api', sidebarId: 'api', position: 'left', label: 'API'},
        {to: '/changelog', label: 'Changelog', position: 'left'},
        {type: 'localeDropdown', position: 'right', className: 'navbar-locale'},
        {href: repo, label: 'GitHub', position: 'right', className: 'navbar-github', 'aria-label': 'GitHub'},
      ],
    },
    footer: {
      style: 'dark',
      links: [{label: 'Vladislav Panin', href: 'https://github.com/VPDPersonal'}, {label: 'Email', href: 'mailto:vpd.aspid@gmail.com'}, {label: 'MIT License', href: `${repo}/blob/main/LICENSE`}],
      copyright: `Copyright © ${new Date().getFullYear()} Vladislav Panin. MIT License.`,
    },
    prism: {theme: venom.light, darkTheme: venom.dark, additionalLanguages: ['csharp', 'json', 'bash']},
  },
};
