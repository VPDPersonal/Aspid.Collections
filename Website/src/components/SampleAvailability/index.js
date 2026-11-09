import React from 'react';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';

export default function SampleAvailability() {
  const {i18n, siteConfig: {customFields}} = useDocusaurusContext();
  if (!customFields.samplesUnreleased) return null;
  return <aside className="collections-availability" role="note">{i18n.currentLocale === 'ru'
    ? `Новые сцены — в Unreleased. Для запуска откройте Aspid.Collections/ из текущей рабочей копии; в опубликованной ${customFields.packageVersion} их пока нет.`
    : `These scenes are in Unreleased. Open Aspid.Collections/ from the current source checkout to try them; published ${customFields.packageVersion} does not include them yet.`}</aside>;
}
