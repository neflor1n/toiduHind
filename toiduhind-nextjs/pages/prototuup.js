import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';

export default function Prototuup() {
  return (
    <>
      <Head>
        <title>Toiduhind – Prototüüp</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta httpEquiv="X-UA-Compatible" content="ie=edge" />
        <meta name="Description" content="Toiduhind prototüübi ja logo esitlus" />
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
            <h1 className="custom-title">Prototüüp</h1>
          </div>
        </header>

        <Navbar />
      </div>
      <main className="custom-wrapper container mt-5">
        <section className="text-center">
          <h2 className="mb-4">Meie Logo</h2>
          <Image src="/assets/img/logo.png" alt="Toiduhind logo" width={300} height={300} className="rounded shadow" />
          <p className="mt-3 text-muted">Lihtne, arusaadav ja toidukorviga sümboliseeritud – meie rakenduse nägu</p>
        </section>
	
	<section className="screenshots-section">
  <h2 className="mb-4">Meie mobiilirakendus</h2>
  <div className="screenshot-row">
    <div className="screenshot-item">
      <h5>Algusleht</h5>
      <Image src="/assets/img/avaleht.png" alt="Avaleht page" width={320} height={700} />
      <p>Rakenduse tervitusleht sisselogimiseks või registreerumiseks.</p>
    </div>
    <div className="screenshot-item">
      <h5>Sisse logimine</h5>
      <Image src="/assets/img/loginForm.png" alt="Login Form" width={320} height={700} />
      <p>Kasutaja saab sisestada oma andmed ja siseneda süsteemi.</p>
    </div>
    <div className="screenshot-item">
      <h5>Registreerimine</h5>
      <Image src="/assets/img/regForm.png" alt="Reg Form" width={320} height={700} />
      <p>Uue kasutaja loomise vorm koos emaili ja parooli kinnitamisega.</p>
    </div>
  </div>

  <div className="screenshot-row mt-5">
    <div className="screenshot-item">
      <h5>Koduleht</h5>
      <Image src="/assets/img/mainPage.png" alt="Main page" width={320} height={700} />
      <p>Toodete ja hindade ülevaade koos sooduspakkumistega.</p>
    </div>
    <div className="screenshot-item">
      <h5>Külgribaga vaade</h5>
      <Image src="/assets/img/mainPageWSlidebar.png" alt="Sidebar view" width={320} height={700} />
      <p>Peamenüü ja kategooriate navigeerimine vasakult ribalt.</p>
    </div>
    <div className="screenshot-item">
      <h5>Seaded</h5>
      <Image src="/assets/img/settings.png" alt="Settings" width={320} height={700} />
      <p>Kasutaja saab muuta oma emaili, parooli ja keelevalikut.</p>
    </div>
  </div>
</section>
	</main>
      <Footer />

    </>
  );
}
