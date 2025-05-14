import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';

export default function Home() {
  return (
    <>
      <Head>
        <title>Projekt Toiduhind.ee</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta httpEquiv="X-UA-Compatible" content="ie=edge" />
        <meta name="Description" content="Enter your description here" />
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
            <h1 className="custom-title">Projekt Toiduhind.ee</h1>
          </div>
        </header>

        <Navbar />
      </div>


      <main className="custom-wrapper container mt-4">
        <section className="intro-section shadow p-4 rounded bg-light">
          <p><strong>Toiduhind</strong> on meie ühisprojekt, mille eesmärk on luua mobiilirakendus toidukaupade hindade võrdlemiseks Eesti suuremates jaekettides.</p>

          <p>Projektis osaleb <strong>3 meeskonnaliiget</strong> – igaüks oma rolli ja vastutusalaga (arendus, disain, andmestruktuur):</p>

          <ul className="team-list">
            <li><i className="fas fa-user-pen"></i> <strong>Vsevolod Tsarev</strong> – disainer / UX</li>
            <li><i className="fas fa-code"></i> <strong>Bogdan Sergachev</strong> – arendaja</li>
            <li><i className="fas fa-database"></i> <strong>Gleb Sotsjov</strong> – andmestruktuur ja loogika</li>
          </ul>

          <p>Projekti juhendab meie õpetaja <strong>Irina Merkulova</strong>, kes tegutseb juhendaja ja koordineerijana, toetades meid nii tehniliselt kui ka metoodiliselt.</p>

          <p>Tegemist on <strong>õppimise eesmärgil loodud praktilise projektiga</strong>, mille kaudu omandame oskusi tarkvaraarenduses, mobiiliarenduses, UX-disainis, andmehalduses ja tiimitöös.</p>

          <div className="text-center mt-5">
          <Image
            src="/assets/img/babka.jpg"
            alt="babuwka"
            width={500}
            height={300}
            className="img-fluid rounded shadow-lg"
          />
          <p className="custom-caption">Allikas: <a href='https://pikabu.ru/story/kak_babka_s_vnukom_ukrala_vodu_i_sukhariki_10827923'>https://pikabu.ru/story/kak_babka_s_vnukom_ukrala_vodu_i_sukhariki_10827923</a></p>
        </div>
        </section>

        
      </main>


      <Footer />
    </>
  );
}
