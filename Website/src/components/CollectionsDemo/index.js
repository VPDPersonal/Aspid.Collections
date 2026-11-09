import React, {useRef, useState} from 'react';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import CodeBlock from '@theme/CodeBlock';

const initial = [{id: 1, name: 'Nova'}, {id: 2, name: 'Mira'}, {id: 3, name: 'Orin'}];
const names = ['Ash', 'Wren', 'Sage', 'Lark', 'Rune'];

export default function CollectionsDemo() {
  const ru = useDocusaurusContext().i18n.currentLocale === 'ru';
  const [items, setItems] = useState(initial);
  const [filter, setFilter] = useState(false);
  const [event, setEvent] = useState('Add / batch: 3');
  const next = useRef(4);
  const add = (count) => {
    const batch = Array.from({length: count}, () => {const id = next.current++; return {id, name: names[(id - 4) % names.length]};});
    setItems(previous => [...previous, ...batch]);
    setEvent(count === 1 ? 'Add / single: 1' : `Add / batch: ${count}`);
  };
  const visible = items.filter(item => !filter || item.id % 2 === 1);
  return <section className="collections-demo" aria-label={ru ? 'Интерактивная иллюстрация коллекций' : 'Interactive collection illustration'}>
    <div className="collections-demo__toolbar">
      <button type="button" disabled={items.length >= 8} onClick={() => add(1)}>{ru ? 'Добавить' : 'Add item'}</button>
      <button type="button" disabled={items.length > 6} onClick={() => add(2)}>{ru ? 'Добавить пару' : 'Add pair'}</button>
      <button type="button" disabled={items.length < 2} onClick={() => {setItems(([first, ...rest]) => [...rest, first]); setEvent('Move / 0 → ' + (items.length - 1));}}>{ru ? 'Переместить первый' : 'Move first'}</button>
      <button type="button" aria-pressed={filter} onClick={() => setFilter(value => !value)}>{ru ? 'Нечётные ID' : 'Odd IDs'}</button>
      <button type="button" disabled={!items.length} onClick={() => {setItems([]); setEvent('Reset');}}>{ru ? 'Очистить' : 'Clear'}</button>
    </div>
    <div className="collections-demo__columns">
      <div><h3>{ru ? 'Источник' : 'Source'} <code>{items.length}</code></h3>
        <div className="collections-demo__tokens">{items.map(item => <span key={item.id}><small>#{item.id}</small>{item.name}</span>)}{!items.length && <p>{ru ? 'Добавьте участника.' : 'Add a squad member.'}</p>}</div>
        <p className="collections-demo__event" aria-live="polite">{event}</p>
      </div>
      <div><h3>{ru ? 'Представление' : 'Derived view'} <code>{visible.length}</code></h3>
        <div className="collections-demo__tokens">{visible.map(item => <span key={item.id}><small>#{item.id}</small>{item.name}</span>)}{!visible.length && <p>{ru ? 'Нет подходящих участников.' : 'No matching members.'}</p>}</div>
        <p className="collections-demo__caption">{ru ? 'ID сохраняются при перемещении. Фильтр не меняет источник.' : 'Moves preserve IDs. Filtering leaves the source unchanged.'}</p>
      </div>
    </div>
    <CodeBlock language="csharp">{'using var members = new ObservableList<string>();\nusing var cards = members.CreateSync(name => new MemberCard(name), isDisposable: true);\nmembers.AddRange("Nova", "Mira"); // One source notification.'}</CodeBlock>
    <p className="collections-demo__caption">{ru ? 'Иллюстрация поведения в браузере. Настоящий C#-пример — Squad Roster в Unity.' : 'A browser illustration of the behavior. Run Squad Roster in Unity for the actual C# sample.'}</p>
  </section>;
}
