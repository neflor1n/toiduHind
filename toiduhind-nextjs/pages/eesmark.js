import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';
import '../styles/eesmark.module.css';

export default function Eesmark() {
  return (
    <>
      <Head>
        <title>Toiduhind - Eesmärk ja Visioon</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta httpEquiv="X-UA-Compatible" content="ie=edge" />
        <meta name="Description" content="Toiduhind.ee eesmärk ja visioon" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
      </Head>

      <header className="full-width-header">
        <div className="header-content">
          <Image
            src="/assets/img/logo.png"
            alt="logo"
            width={60}
            height={60}
            className="logo-img"
          />
          <h1 className="custom-title">Toiduhind - Eesmärk ja Visioon</h1>
        </div>
      </header>

      <Navbar />

      <main className="custom-wrapper container mt-5">
        <section className="mb-5">
          <h2 className="text-center">Mis on Toiduhind.ee?</h2>
          <p className="lead text-center">
            <strong>Toiduhind.ee</strong> on mobiilirakendus, mis aitab võrrelda toidukaupade hindu erinevates Eesti poodides – nagu Selver, Prisma, Maxima, Coop ja teised. Nii saab leida kiirelt parimad hinnad, olles kas kodus või poes.
          </p>
        </section>

        <h2>Meie eesmärk:</h2>
        
        <section className="eesmark-section d-flex flex-wrap gap-4 mb-5">
          <div className="flex-grow-1">
            <h3 className="text-center">Miks valida Toiduhind.ee?</h3>
            <Image src="/assets/img/aidataInimestel.png" width={400} height={300} alt="aidata inimestel" className="rounded shadow" />
            <ul className="goal-list mt-3">
              <li>➡️ Aidata inimestele</li>
              <li>➡️ Säästa raha</li>
              <li>➡️ Leida parimad pakkumised</li>
              <li>➡️ Teha teadlikke valikuid</li>
              <li>➡️ Jälgida hindade muutumist ajas</li>
            </ul>
          </div>

          <div className="flex-grow-1">
            <h3 className="text-center">Peamised Funktsioonid</h3>
            <Image src="/assets/img/peamised.png" width={400} height={300} alt="peamised funktsioonid" className="rounded shadow" />
            <ul className="goal-list mt-3">
              <li>➡️ Hindade võrdlus poodide lõikes</li>
              <li>➡️ Tooteotsing ja filtreerimine</li>
              <li>➡️ Hinnamuutuste ajalugu graafikutega</li>
              <li>➡️ Ostunimekiri koos hinnainfo ja soodushindade teavitustega</li>
              <li>➡️ Näitab, kus toode on kõige odavam sinu lähedal</li>
            </ul>
          </div>
        </section>

        <section className="future-section">
          <h2 className="text-center">Tulevikuplaanid</h2>
          <div className="text-center">
            <Image src="/assets/img/tulevikuplaanid.png" width={400} height={300} alt="tulevikuplaanid" className="rounded shadow" />
          </div>
          <ul className="goal-list mt-3 text-center">
            <li>➡️ Tulevikuplaanide hinnakalkulaator</li>
            <li>➡️ Hinnakalkulaator poodide lõikes</li>
            <li>➡️ Hindade võrdlus poodide lõikes</li>
            <li>➡️ Koostöö kohalike poekettidega</li>
            <li>➡️ Isikupärastatud teavitused kasutajale</li>
          </ul>
        </section>
      </main>
    </>
  );
}
