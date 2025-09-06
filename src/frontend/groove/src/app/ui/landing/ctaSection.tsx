import Link from "next/link";

export default function CTASection() {
  return (
    <section id="pricing" className="bg-slate-900">
      <div className="mx-auto max-w-6xl px-4 py-16 text-center md:px-6 md:py-24">
        <h2 className="text-3xl font-bold tracking-tight text-white md:text-4xl">
          Ready to book smarter?
        </h2>
        <p className="mx-auto mt-3 max-w-2xl text-slate-300">
          Start free. Upgrade when you need team seats, contracts and payouts.
        </p>
        <div className="mt-8 flex justify-center gap-3">
          <Link
            href="/signup"
            className="rounded-full bg-fuchsia-600 px-6 py-3 font-semibold text-white hover:bg-fuchsia-500"
          >
            Create account
          </Link>
          <Link
            href="/demo"
            className="rounded-full border border-white/20 px-6 py-3 text-slate-100 hover:border-fuchsia-300 hover:text-white"
          >
            Talk to us
          </Link>
        </div>
      </div>
    </section>
  );
}
