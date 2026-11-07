import React from 'react';
import MDXComponents from '@theme-original/MDXComponents';
import IntroBanner from '@site/src/components/IntroBanner';
import InstallPanel from '@site/src/components/InstallPanel';
import SupportPanel from '@site/src/components/SupportPanel';
import StatusBadge from '@site/src/components/StatusBadge';
import CollectionsDemo from '@site/src/components/CollectionsDemo';
import SampleAvailability from '@site/src/components/SampleAvailability';
import SamplePreview from '@site/src/components/SamplePreview';
function DocTable(props) { return <div className="doc-table-scroll"><table {...props} /></div>; }
export default {...MDXComponents, table: DocTable, IntroBanner, InstallPanel, SupportPanel, StatusBadge, CollectionsDemo, SampleAvailability, SamplePreview};
