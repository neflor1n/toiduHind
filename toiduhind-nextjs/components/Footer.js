import Link from 'next/link';

export default function Footer() {
    return (
        <footer className="footer">
            <div className="footer-content">
                <p>© {new Date().getFullYear()} <strong>Toiduhind.ee</strong></p>
                <p>
                    Projekti lõi {""}
                    <Link href={'https://taltech.ee/infotehnoloogia-teaduskond/iduprojekt'}>Idu Projektile</Link>
                    {" "}
                    <Link href={'https://tthk.ee/'}>Tallinna Tööstushariduskeskuse</Link> meeskond
                </p>
            </div>
            <div className='footer-second-content'>
                <div className='kontaktandmed'>
                    <p>
                        <strong>Kontantandmed:</strong>  
                    </p>
                    <p>
                        Info: <em>lanor1n23@gmail.com</em>
                    </p>
                </div>
                <div className='meeskonnaliikmed'>
                    <p>
                        <strong>Meeskonnaliikmed:</strong>
                    </p>
                    <ul style={{listStyleType: 'none'}}>
                            <li><i class="fa-solid fa-arrow-right"></i> <strong>Bogdan Sergachev</strong> - arendaja</li>
                            <li><i class="fa-solid fa-arrow-right"></i> <strong>Vsevolod Tsarev</strong> - UX / design</li>
                            <li><i class="fa-solid fa-arrow-right"></i> <strong>Gleb Sõtsjov</strong> - andmestruktuur ja loogika</li>
                        </ul>
                </div>
            </div>
        </footer>
    );
}
