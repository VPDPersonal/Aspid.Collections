import React from 'react';
import Link from '@docusaurus/Link';
import useBaseUrl from '@docusaurus/useBaseUrl';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import ThemedImage from '@theme/ThemedImage';
import styles from './styles.module.css';
import SampleAvailability from '../SampleAvailability';
const samples = [
  ['squad-roster', 'Squad Roster', 'Recruit, reorder and release squad cards.', 'Добавление, перестановка и освобождение карточек отряда.'],
  ['item-browser', 'Item Browser', 'Search, categorize and sort a catalog.', 'Поиск, категории и сортировка одного каталога.'],
  ['crafting-queue', 'Crafting Queue', 'Queue work orders and undo the latest completion.', 'Очередь заказов и отмена последнего завершения.'],
  ['match-scoreboard', 'Match Scoreboard', 'Update keyed scores and watch the ranking.', 'Счёт игроков по ID с обновлением отдельных строк.'],
  ['zone-occupancy', 'Zone Occupancy', 'Unique visitors and a detachable alarm.', 'Уникальные посетители и отключаемая сигнализация.'],
];
function Card({sample, index, ru}) {
  const [id, title, en, russian] = sample;
  const dark = useBaseUrl(`/img/samples/${id}.png`);
  const light = useBaseUrl(`/img/samples/${id}-light.png`);
  return <Link to={`/tutorials/${id}`} className={styles.card}>
    <div className={styles.preview}><ThemedImage sources={{dark, light}} alt={title} width="1600" height="1000" loading={index < 2 ? 'eager' : 'lazy'} /></div>
    <div className={styles.content}><p className={styles.feature}>{title}</p><p>{ru ? russian : en}</p></div>
  </Link>;
}
export default function SamplesGallery() {
  const ru = useDocusaurusContext().i18n.currentLocale === 'ru';
  return <div className={styles.page}>
    <header className={styles.header}><h1>{ru ? 'Примеры' : 'Samples'}</h1><p>{ru
      ? 'Пять готовых сцен Unity. Импортируйте нужную через Package Manager.'
      : 'Five ready-to-play Unity scenes. Import any one through Package Manager.'}</p></header>
    <SampleAvailability />
    <div className={styles.grid}>{samples.map((sample, index) => <Card key={sample[0]} sample={sample} index={index} ru={ru} />)}</div>
  </div>;
}
