export default function Footer() {
  return (
    <footer id="contact" className="border-t border-white/10 bg-slate-950">
      <div className="mx-auto max-w-6xl px-4 py-10 md:px-6">
        <div className="flex flex-col items-start justify-between gap-6 md:flex-row md:items-center">
          <div className="flex items-center gap-3">
            <span className="inline-grid h-8 w-8 place-items-center rounded-full bg-fuchsia-600 text-white font-bold">
              gr
            </span>
            <span className="text-white">groove</span>
          </div>
          <nav className="flex gap-6 text-sm">
            <a href="#product" className="text-slate-300 hover:text-white">
              Product
            </a>
            <a href="#features" className="text-slate-300 hover:text-white">
              Features
            </a>
            <a
              href="/legal/privacy"
              className="text-slate-300 hover:text-white"
            >
              Privacy
            </a>
            <a
              href="/legal/imprint"
              className="text-slate-300 hover:text-white"
            >
              Imprint
            </a>
          </nav>
        </div>
        <p className="mt-6 text-xs text-slate-400">
          © {new Date().getFullYear()} Groove — All rights reserved.
        </p>
      </div>
    </footer>
  );
}
