import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';

export default function Eesmark() {
    return(
        <>
            <Head>
                <title>Eesmärk ja Visioon</title>
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
                <link rel="stylesheet" href="/assets/css/style.css" />
            </Head>
            <main className="container mt-4">
                <h1>Toiduhind - Eesmärk ja Visioon</h1>

                <Navbar />
                <p><strong>Mis on Toiduhind.ee</strong></p>

                <p><strong>Toiduhind.ee</strong> on mobiilirakendus, mis aitab võrrelda toidukaupade hindu erinevates Eesti poodides – nagu Selver, Prisma, Maxima, Coop ja teised. Nii saab leida kiirelt parimad hinnad, olles kas kodus või poes.</p>

                <h2>Meie Eesmärk:</h2>

                <div className="eesmark" id='eesmark'> 
                    <div className='eesmark-aidata'>
                        <h2>AIDATA INIMESTEL</h2>
                        <Image 
                            src='/assets/img/aidataInimestel.png'
                            width={400}
                            height={300}
                            alt="aidata inimestel"
                        />
                        <div className='eesmark-aidata-info'>
                            <ul style={{listStyleType: 'none'}}>
                                <li>➡️ Säästa raha</li>
                                <li>➡️ Leida parimad pakkumised</li>
                                <li>➡️ Teha teadlikke valikuid</li>
                                <li>➡️ Jälgida hindade muutumist ajas</li>
                            </ul>
                        </div>
                    </div>
                    <div className='eesmark-peamised-funktsioonid' id='eesmark-peamised-funktsioonid'>
                    <h2>PEAMISED FUNKTSIOONID</h2>
                    <Image 
                        src="/assets/img/peamised.png"
                        width={400}
                        height={300}
                        alt="peamised funktsioonid"
                    />
                    <div className='eesmark-peamised-funktsioonid-info'>
                        <ul style={{listStyleType: 'none'}}>
                            <li>➡️ Hindade võrdlus poodide lõikes</li>
                            <li>➡️ Tooteotsing ja filtreerimine</li>
                            <li>➡️ Hinnamuutuste ajalugu graafikutega</li>
                            <li>➡️ Ostunimekiri koos hinnainfo ja soodushindade teavitustega</li>
                            <li>➡️ Näitab, kus toode on kõige odavam sinu lähedal</li>
                        </ul>
                    </div>
                    </div>
                </div>

                <div className='tulevikuplaanid' id='tulevikuplaanid'>
                    <h2>TULEVIKUPLAANID</h2>
                    <Image 
                        src="/assets/img/tulevikuplaanid.png"
                        width={400}
                        height={300}
                        alt="tulevikuplaanid"
                    />
                    <div className='tulevikuplaanid-info'>
                        <ul style={{listStyleType: 'none'}}>
                            <li>➡️ Tulevikuplaanide hinnakalkulaator</li>
                            <li>➡️ Hinnakalkulaator poodide lõikes</li>
                            <li>➡️ Hindadeõrdlus poodide lõikes</li>
                            <li>➡️ Hindadeõrdlus poodide lõikes</li>
                            <li>➡️ Hindadeõrdlus poodide lõikes</li>
                        </ul>
                    </div>
                </div>

                

            </main>
        </>
    )
}