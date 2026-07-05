import { useEffect, useState } from "react";
import { getPrompts } from "./api/promptsApi";
import PromptForm from "./components/PromptForm";
import PromptList from "./components/PromptList";
import type { PromptItemData } from "./types";

export default function App() {
  const [prompts, setPrompts] = useState<PromptItemData[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const load = async () => {
    try {
      setLoading(true);
      const data = await getPrompts();
      setPrompts(data);
      setError(null);
    } catch {
      setError("Failed to load prompts. Please check the backend connection.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void load();

    const intervalId = window.setInterval(() => {
      void load();
    }, 4000);

    return () => window.clearInterval(intervalId);
  }, []);

  return (
    <div className="app-shell">
      <div className="dashboard-grid">
        <PromptForm onCreated={load} />

        <section className="panel">
          <div className="panel-header">
            <div>
              <p className="panel-kicker">Monitoring</p>
              <h2>Prompt list</h2>
            </div>
            {loading ? (
              <span className="status-pill pending">Refreshing…</span>
            ) : null}
          </div>

          {error ? <div className="error-banner">{error}</div> : null}
          <PromptList prompts={prompts} />
        </section>
      </div>
    </div>
  );
}
