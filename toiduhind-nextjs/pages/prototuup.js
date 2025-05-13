import Head from 'next/head';
import Image from 'next/image';
import Navbar from '../components/Navbar';

export default function Prototuup() {
    return(
        <>
            <Head>
                <title>Prototüüp</title>
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
                <h1>Prototüüp</h1>

                <Navbar />

                <div className='meieLogo' id='meieLogo'>
                    <h2>Meie Logo</h2>
                    <Image src="/assets/img/logo.png" alt="logo" width={400} height={400} />
                </div>

            </main>
        </>
    )
}