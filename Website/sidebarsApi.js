import generated from './api/sidebar.js';

export default {api: generated.api.map(section => ({...section,
  label: section.label.replace(/^Aspid\.Collections\./, ''),
  className: 'doc-menu-group api-namespace',
  collapsed: true,
  items: section.items.flatMap(item => item.type === 'category' ? item.items : [item]),
}))};
