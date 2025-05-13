// pages/_app.js
import '../styles/globals.css';
import '../styles/navbar.css';
import '../styles/eesmark.css';
import '../styles/eeskujud.css';
import '../styles/prototuup.css';
import '../styles/custom.css';

export default function MyApp({ Component, pageProps }) {
  return <Component {...pageProps} />;
}
