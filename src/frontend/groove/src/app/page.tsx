import Navbar from "./ui/landing/navbar";
import TopBanner from "./ui/landing/topBanner";
import Hero from "./ui/landing/hero";
import FeatureCards from "./ui/landing/featureCards";
import HowItWorks from "./ui/landing/howItWorks";
import Stats from "./ui/landing/stats";
import CTASection from "./ui/landing/ctaSection";
import Footer from "./ui/landing/footer";

export default function Page() {
  return (
    <main className="min-h-screen bg-slate-950">
      <TopBanner />
      <Navbar />
      <Hero />
      <FeatureCards />
      <HowItWorks />
      <Stats />
      <CTASection />
      <Footer />
    </main>
  );
}
