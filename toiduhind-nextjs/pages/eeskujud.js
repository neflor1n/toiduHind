import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';
import Footer from '../components/Footer';
export default function Eeskujud() {
  return (
    <>
      <Head>
        <title>Eeskujud ja Võrdlus</title>
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
	    <h1 className="custom-title">Toiduhind.ee</h1>
            <h1 className="custom-title" style={{marginLeft: '70px'}}>Eeskujud ja Võrdlus</h1>
          </div>
        </header>

        <Navbar />
      </div>

      <main className="custom-wrapper container mt-5">
        <section className="eeskuju-section d-flex gap-4 mb-5">
          <Image src="/assets/img/hinnavaatlus.png" alt="Hinnavaatlus" width={300} height={600} className="rounded shadow" />
          <div className='eeskuju-section-content'>
            <h3>1. Hinnavaatlus.ee</h3>
            <p><strong>Hinnavaatlus</strong> on üks vanimaid ja tuntumaid hinnavõrdlusportaale Eestis...</p>
            <div>
              <h5>Eelised:</h5>
              <ul>
                <li>Väga detailne hinnainfo</li>
                <li>Ajaloolised hinnagraafikud</li>
                <li>Kasutajate arvustused ja foorumid</li>
                <li>Hinnateavitused</li>
              </ul>
              <h5>Puudused:</h5>
              <ul>
                <li>Ei hõlma toidukaupu</li>
                <li>Ei paku reaalajas hindu</li>
              </ul>
            </div>
          </div>
        </section>

        <section className="eeskuju-section d-flex gap-4 mb-5">
          <Image src="/assets/img/hindee.png" alt="Hind.ee" width={300} height={600} className="rounded shadow" />
          <div className='eeskuju-section-content'>
            <h3>2. Hind.ee</h3>
            <p><strong>Hind.ee</strong> keskendub kampaaniapakkumistele ja soodushindadele.</p>
            <div>
              <h5>Eelised:</h5>
              <ul>
                <li>Kiire ülevaade pakkumistest</li>
                <li>Lihtne navigeerimine</li>
                <li>Otsing ja kategooriad</li>
              </ul>
              <h5>Puudused:</h5>
              <ul>
                <li>Näitab ainult ühe poe hindu</li>
                <li>Pole hinnamuutuste ajalugu</li>
              </ul>
            </div>
          </div>
        </section>

        <section className="comparison-section mt-5">
          <h2 className="text-center">Võrdlustabel: Toiduhind.ee vs Teised</h2>
          <div className="table-responsive mt-3">
            <table className="table table-bordered table-hover text-center">
              <thead className="table-dark">
                <tr>
                  <th>Funktsioon</th>
                  <th>Hinnavaatlus.ee</th>
                  <th>Hind.ee</th>
                  <th>Toiduhind.ee</th>
                </tr>
              </thead>
              <tbody>
                <tr><td>Toidukaupade fookus</td><td>Ei</td><td>Osaliselt</td><td>Jah</td></tr>
                <tr><td>Reaalajas hinnad</td><td>Osaliselt</td><td>Piiratud</td><td>Jah</td></tr>
                <tr><td>Hinnamuutuste ajalugu</td><td>Jah</td><td>Ei</td><td>Jah</td></tr>
                <tr><td>Sooduspakkumised</td><td>Jah</td><td>Jah</td><td>Jah</td></tr>
                <tr><td>Kasutajate hinnasisestus</td><td>Ei</td><td>Ei</td><td>Jah (plaanis)</td></tr>
                <tr><td>Mobiilirakendus</td><td>Jah</td><td>Ei</td><td>Jah (töös)</td></tr>
                <tr><td>Kulleri teenus</td><td>Ei</td><td>Ei</td><td>Jah</td></tr>
                <tr><td>Avalik API</td><td>Piiratud</td><td>Ei</td><td>Jah (tulemas)</td></tr>
                <tr><td>Geograafiline täpsus</td><td>Ei</td><td>Ei</td><td>Jah (plaanis)</td></tr>
                <tr><td>Supermarketite võrdlus</td><td>Ei</td><td>Ei</td><td>Jah</td></tr>
              </tbody>
            </table>
          </div>
        </section>
      </main>

      <Footer />

    </>
  );
}
