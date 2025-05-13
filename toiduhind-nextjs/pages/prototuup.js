import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';
import '../styles/prototuup.css';

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

      <header className="full-width-header">
        <div className="header-content">
          <Image src="/assets/img/logo.png" alt="logo" width={60} height={60} className="logo-img" />
          <h1 className="custom-title">Prototüüp</h1>
        </div>
      </header>

      <Navbar />

      <main className="custom-wrapper container mt-5">
        <section className="text-center">
          <h2 className="mb-4">Meie Logo</h2>
          <Image src="/assets/img/logo.png" alt="Toiduhind logo" width={300} height={300} className="rounded shadow" />
          <p className="mt-3 text-muted">Lihtne, arusaadav ja toidukorviga sümboliseeritud – meie rakenduse nägu</p>
        </section>
      </main>
    </>
  );
}
