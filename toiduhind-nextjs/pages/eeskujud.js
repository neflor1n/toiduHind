import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';

export default function Eeskujud() {
  return (
    <>
      <Head>
        <title>Eeskujud ja Võrdlus</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta httpEquiv="X-UA-Compatible" content="ie=edge" />
        <meta name="Description" content="Enter your description here" />
        <link
          rel="stylesheet"
          href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.min.css"
        />
        <link
          rel="stylesheet"
          href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css"
        />
        <link rel="stylesheet" href="public/assets/css/style.css" />
      </Head>

      <main className="container mt-4">
        <h1>Eeskujud ja Võrdlus</h1>
        
        <Navbar />

        <h2>Eeskujud Eestis</h2>

        <p>
          Projekti Toiduhind.ee arendamisel oleme uurinud olemasolevaid lahendusi Eestis, et mõista, millised funktsioonid töötavad hästi,
          millised on kasutajate ootused ning millised lüngad vajavad täitmist.
        </p>

        <div className="hinnavaatlus" id="hinnavaatlus">
            <Image
                src="/assets/img/hinnavaatlus.png"
                alt="eeskujud"
                width={300}
                height={600}
            />
            <div className='hinnavaatlusInfo' id='hinnavaatlusInfo'>
                <h3>1. Hinnavaatlus.ee</h3>
                <p>
                <strong>Hinnavaatlus</strong> on üks vanimaid ja tuntumaid hinnavõrdlusportaale Eestis. Selle fookus on peamiselt tehnikatoodetele – arvutid, telefonid, telerid, kodumasinad jne.
                </p>
                <div className="hinnavaatlusEelised" id="hinnavaatlusEelised">
                    <p><strong>Eelised:</strong></p>
                    <ul>
                        <li>Väga detailne hinnainfo paljude edasimüüjate kohta</li>
                        <li>Ajaloolised hinnagraafikud</li>
                        <li>Kasutajate arvustused ja foorumid</li>
                        <li>Võimalus seadistada hinnateavitus</li>
                    </ul>
                </div>
                <div className='hinnavaatlusPuudused' id='hinnavaatlusPuudused'>
                    <p><strong>Puudused:</strong></p>
                    <ul>
                        <li>Ei hõlma suuremat hulka toidukaupu</li>
                        <li>Ei paku reaalajas toiduainete hindu ega võrdlusi</li>
                    </ul>
                </div>
            </div>
        </div>
        <div className='hindee'>
            <Image
                src="/assets/img/hindee.png"
                alt="eeskujud"
                width={300}
                height={600}
            />
            <div className='hindeeInfo' id='hindeeInfo'>
                <h3>2. Hind.ee</h3>
                <p>
                <strong>Hind.ee</strong> on lihtsam hinnavõrdlussait, mis keskendub eelkõige kampaaniapakkumistele ja soodushindadele erinevates e-poodides.
                </p>
                <div className='hindeeEelised' id='hindeeEelised'>
                    <p><strong>Eelised:</strong></p>
                    <ul>
                        <li>Kiire ülevaade sooduspakkumistest</li>
                        <li>Kerge navigeerida</li>
                        <li>Toodete otsing ja kategooriate sirvimine</li>
                    </ul>
                </div>
                <div className='hindeePuudused' id='hindeePuudused'>
                    <p><strong>Puudused:</strong></p>
                    <ul>
                        <li>Näitab kõige odavamat kaupa ühe poe lõikes</li>
                        <li>Ei paku hinnamuutuste ajalugu</li>
                    </ul>
                </div>
            </div>
        </div>
            <br></br>
            <br></br>
            <h2>Võrdlus: Toiduhind.ee vs Teised Lahendused</h2>
            <table className="table table-bordered">
                <thead className="thead-dark">
                    <tr>
                    <th>Funktsioon</th>
                    <th>Hinnavaatlus.ee</th>
                    <th>Hind.ee</th>
                    <th><strong>Toiduhind.ee</strong></th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                    <td>Toidukaupade fookus</td>
                    <td>Ei</td>
                    <td>Osaliselt</td>
                    <td>Jah</td>
                    </tr>
                    <tr>
                    <td>Reaalajas hinnad</td>
                    <td>Osaliselt</td>
                    <td>Piiratud</td>
                    <td>Jah</td>
                    </tr>
                    <tr>
                    <td>Hinnamuutuste ajalugu</td>
                    <td>Jah</td>
                    <td>Ei</td>
                    <td>Jah</td>
                    </tr>
                    <tr>
                    <td>Sooduspakkumised</td>
                    <td>Jah</td>
                    <td>Jah</td>
                    <td>Jah</td>
                    </tr>
                    <tr>
                    <td>Kasutajate hinnasisestus</td>
                    <td>Ei</td>
                    <td>Ei</td>
                    <td>Jah (plaanis)</td>
                    </tr>
                    <tr>
                    <td>Mobiilirakendus</td>
                    <td>Jah</td>
                    <td>Ei</td>
                    <td>Jah (töös)</td>
                    </tr>
                    <tr>
                    <td>Kulleri teenus</td>
                    <td>Ei</td>
                    <td>Ei</td>
                    <td>Jah</td>
                    </tr>
                    <tr>
                    <td>Avalik API arendajatele</td>
                    <td>Piiratud</td>
                    <td>Ei</td>
                    <td>Jah (tulemas)</td>
                    </tr>
                    <tr>
                    <td>Geograafiline täpsus (poe tase)</td>
                    <td>Ei</td>
                    <td>Ei</td>
                    <td>Jah (plaanis lisada)</td>
                    </tr>
                    <tr>
                    <td>Fookus supermarketite võrdlusel</td>
                    <td>Ei</td>
                    <td>Ei</td>
                    <td>Jah</td>
                    </tr>
                </tbody>
            </table>
      </main>   
    </>
  );
}
