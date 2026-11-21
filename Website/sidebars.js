const group = (label, items) => ({type: 'category', className: 'doc-menu-group', collapsible: false, label, items});
export default {docs: ['intro', group('Getting started', ['installation', 'quick-start']),
  group('Working with changes', ['events', 'synchronization', 'filtering', 'lifetime']), 'troubleshooting']};
