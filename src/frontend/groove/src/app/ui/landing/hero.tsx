import Link from "next/link";

export default function Hero() {
  return (
    <section className="relative overflow-hidden bg-slate-900">
      {/* soft radial accents */}
      <div className="pointer-events-none absolute inset-0">
        <div className="absolute -top-40 -left-40 h-80 w-80 rounded-full bg-fuchsia-600/20 blur-3xl" />
        <div className="absolute -bottom-40 -right-40 h-96 w-96 rounded-full bg-fuchsia-400/20 blur-3xl" />
      </div>

      <div className="relative mx-auto max-w-6xl px-4 py-20 md:px-6 md:py-28">
        <p className="mb-4 inline-flex rounded-full border border-white/10 bg-white/5 px-3 py-1 text-xs text-slate-200">
          For organizers, venues & artists
        </p>
        <h1 className="max-w-2xl text-5xl font-extrabold tracking-tight text-white sm:text-6xl">
          Connect. <span className="text-fuchsia-400">Book.</span> Groove.
        </h1>
        <p className="mt-6 max-w-2xl text-lg leading-relaxed text-slate-300">
          Booking, applications, and payments — all digital and seamless. Find
          the right artist, manage requests, and handle payouts in one place.
        </p>

        <div className="mt-8 flex flex-wrap gap-3">
          <Link
            href="/signup"
            className="rounded-full bg-fuchsia-600 px-6 py-3 text-base font-semibold text-white hover:bg-fuchsia-500"
          >
            Get started free
          </Link>
          <Link
            href="/demo"
            className="rounded-full border border-white/20 px-6 py-3 text-base text-slate-100 hover:border-fuchsia-300 hover:text-white"
          >
            Get a demo
          </Link>
        </div>
      </div>
    </section>
  );
}
