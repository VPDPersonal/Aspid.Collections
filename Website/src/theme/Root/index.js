import React from 'react';
import CopyShine from '../../components/CopyShine';
import DotAmbient from '../../components/DotAmbient';
import DotRipple from '../../components/DotRipple';
import DotSpotlight from '../../components/DotSpotlight';
export default function Root({children}) {
  return <><DotAmbient /><DotSpotlight /><DotRipple /><CopyShine />{children}</>;
}
