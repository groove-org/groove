export default function Stats() {
  return (
    <section className="bg-slate-950">
      <div className="mx-auto max-w-6xl px-4 py-14 md:px-6 md:py-20">
        <div className="grid gap-6 rounded-2xl border border-white/10 bg-gradient-to-br from-slate-900 to-slate-950 p-8 md:grid-cols-3">
          {[
            ["3k+", "Artists"],
            ["1.2k+", "Venues"],
            ["15k+", "Bookings processed"],
          ].map(([num, label]) => (
            <div key={label} className="text-center">
              <div className="text-4xl font-extrabold tracking-tight text-white">
                {num}
              </div>
              <div className="mt-1 text-sm uppercase tracking-wide text-slate-300">
                {label}
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
}
