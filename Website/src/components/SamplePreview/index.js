import React from 'react';
import useBaseUrl from '@docusaurus/useBaseUrl';
import DocImage from '@theme/MDXComponents/Img';

export default function SamplePreview({sample, alt}) {
  const dark = useBaseUrl(`/img/samples/${sample}.png`);
  const light = useBaseUrl(`/img/samples/${sample}-light.png`);
  return <div className="sample-preview">
    <span className="theme-image--dark"><DocImage src={dark} alt={alt} width="1600" height="1000" /></span>
    <span className="theme-image--light"><DocImage src={light} alt={alt} width="1600" height="1000" /></span>
  </div>;
}
