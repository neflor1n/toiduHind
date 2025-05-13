import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';

export default function Home() {
  return (
    <>
      <Head>
        <title>Projekt Toiduhind.ee</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta httpEquiv="X-UA-Compatible" content="ie=edge" />
        <meta name="Description" content="Enter your description here" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.min.css" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
      </Head>

      <main className="container mt-4">
        <h1>Projekt Toiduhind.ee</h1>
        <Navbar />

        <p><strong>Toiduhind</strong> on meie ühisprojekt, mille eesmärk on luua mobiilirakendus toidukaupade hindade võrdlemiseks Eesti suuremates jaekettides.</p>

        <p>Projektis osaleb <strong>3 meeskonnaliiget</strong> – igaüks oma rolli ja vastutusalaga (arendus, disain, andmestruktuur):</p>

        <ul>
          <li><strong>Vsevolod Tsarev</strong> – disainer / UX</li>
          <li><strong>Bogdan Sergachev</strong> – arendaja</li>
          <li><strong>Gleb Sotsjov</strong> – andmestruktuur ja loogika</li>
        </ul>

        <p>Projekti juhendab meie õpetaja <strong>mrs. Merkulova</strong>, kes tegutseb juhendaja ja koordineerijana, toetades meid nii tehniliselt kui ka metoodiliselt.</p>

        <p>Tegemist on <strong>õppimise eesmärgil loodud praktilise projektiga</strong>, mille kaudu omandame oskusi tarkvaraarenduses, mobiiliarenduses, UX-disainis, andmehalduses ja tiimitöös.</p>

        <div className="text-center">
          <Image
            src="/assets/img/babka.jpg"
            alt="babuwka"
            width={500}
            height={300}
            className="img-fluid"
          />
          <p style={{ fontSize: '12px' }}>Allikas: https://pikabu.ru/story/kak_babka_s_vnukom_ukrala_vodu_i_sukhariki_10827923</p>
        </div>
      </main>
    </>
  );
}
