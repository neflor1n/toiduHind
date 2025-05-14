import Link from 'next/link';

export default function Footer() {
    return (
        <footer className="footer">
            <div className="footer-content">
                <p>© {new Date().getFullYear()} <strong>Toiduhind.ee</strong></p>
                <p>
                    Projekt on loonud{" "}
                    <Link href="https://tthk.ee" target="_blank" rel="noopener noreferrer">
                        Tallinna Tööstushariduskeskusses (TTHK)
                    </Link>
                </p>
            </div>
        </footer>
    );
}
