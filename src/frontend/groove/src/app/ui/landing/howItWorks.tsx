const steps = [
  {
    k: "01",
    title: "Create your profile",
    text: "Organizer, venue, or artist—set preferences, connect payouts, and import media.",
  },
  {
    k: "02",
    title: "Post or search",
    text: "Publish a gig with date, budget, and rider—or search artists by criteria.",
  },
  {
    k: "03",
    title: "Review & confirm",
    text: "Compare applications side by side, auto-generate contracts, confirm with one click.",
  },
  {
    k: "04",
    title: "Get paid, perform",
    text: "Deposits and final payments handled safely. Ratings close the loop.",
  },
];

export default function HowItWorks() {
  return (
    <section id="product" className="bg-slate-900">
      <div className="mx-auto max-w-6xl px-4 py-16 md:px-6 md:py-24">
        <h2 className="text-3xl font-bold tracking-tight text-white md:text-4xl">
          How Groove works
        </h2>
        <p className="mt-3 max-w-2xl text-slate-300">
          A single flow from discovery to payout.
        </p>

        <ol className="mt-10 grid gap-6 md:grid-cols-2">
          {steps.map((s) => (
            <li
              key={s.k}
              className="relative rounded-2xl border border-white/10 bg-slate-950 p-6"
            >
              <span className="text-sm font-mono text-fuchsia-400">{s.k}</span>
              <h3 className="mt-2 text-xl font-semibold text-white">
                {s.title}
              </h3>
              <p className="mt-2 text-slate-300">{s.text}</p>
            </li>
          ))}
        </ol>
      </div>
    </section>
  );
}
