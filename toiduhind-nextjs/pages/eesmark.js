import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';

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

      <div className="header-wrapper">
        <header className="full-width-header">
          <div className="header-content">
            <Image
              src="/assets/img/toiduhindWhite.png"
              alt="logo"
              width={80}
              height={80}
              className="logo-img"
            />
	    <h1 className="custom-title">Toiduhind.ee</h1>
            <h1 className="custom-title" style={{marginLeft: '70px'}}>Toiduhind - Eesmärk ja Visioon</h1>
          </div>
        </header>

        <Navbar />
      </div>

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
              <li><i className="fa-solid fa-arrow-right"></i> Aidata inimestele</li>
              <li><i className="fa-solid fa-arrow-right"></i> Säästa raha</li>
              <li><i className="fa-solid fa-arrow-right"></i> Leida parimad pakkumised</li>
              <li><i className="fa-solid fa-arrow-right"></i> Teha teadlikke valikuid</li>
              <li><i className="fa-solid fa-arrow-right"></i> Jälgida hindade muutumist ajas</li>
            </ul>
          </div>

          <div className="flex-grow-1">
            <h3 className="text-center">Peamised Funktsioonid</h3>
            <Image src="/assets/img/peamised.png" width={400} height={300} alt="peamised funktsioonid" className="rounded shadow" />
            <ul className="goal-list mt-3">
              <li><i className="fa-solid fa-arrow-right"></i> Hindade võrdlus poodide lõikes</li>
              <li><i className="fa-solid fa-arrow-right"></i> Tooteotsing ja filtreerimine</li>
              <li><i className="fa-solid fa-arrow-right"></i> Hinnamuutuste ajalugu graafikutega</li>
              <li><i className="fa-solid fa-arrow-right"></i> Ostunimekiri koos hinnainfo ja soodushindade teavitustega</li>
              <li><i className="fa-solid fa-arrow-right"></i> Näitab, kus toode on kõige odavam sinu lähedal</li>
            </ul>
          </div>
        </section>

        <section className="future-section">
          <h2 className="text-center">Tulevikuplaanid</h2>
          <div className="text-center">
            <Image src="/assets/img/tulevikuplaanid.png" width={400} height={300} alt="tulevikuplaanid" className="rounded shadow" />
          </div>
          <ul className="goal-list mt-3 text-center">
            <li><i className="fa-solid fa-arrow-right"></i> Tulevikuplaanide hinnakalkulaator</li>
            <li><i className="fa-solid fa-arrow-right"></i> Hinnakalkulaator poodide lõikes</li>
            <li><i className="fa-solid fa-arrow-right"></i> Hindade võrdlus poodide lõikes</li>
            <li><i className="fa-solid fa-arrow-right"></i> Koostöö kohalike poekettidega</li>
            <li><i className="fa-solid fa-arrow-right"></i> Isikupärastatud teavitused kasutajale</li>
          </ul>
        </section>
      </main>

      <Footer />

    </>
  );
}
