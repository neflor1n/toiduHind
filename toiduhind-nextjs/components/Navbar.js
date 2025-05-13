import Link from 'next/link';

export default function Navbar() {
  return (
    <nav>
      <ul>
        <li><Link href="/">Projekt Toiduhind.ee</Link></li>
        <li><Link href="/eeskujud">Eeskujud ja Võrdlus</Link></li>
        <li><Link href="/eesmark">Toiduhind.ee – Eesmärk ja Visioon</Link></li>
        <li><Link href="/prototuup">Prototüüp</Link></li>
      </ul>
    </nav>
  );
}
