import type { PromptItemProps, PromptStatus } from "../types";
import { normalizePromptStatus } from "../types";

const statusMeta: Record<PromptStatus, { label: string; tone: string }> = {
  0: { label: "Pending", tone: "pending" },
  1: { label: "In progress", tone: "processing" },
  2: { label: "Completed", tone: "completed" },
  3: { label: "Failed", tone: "failed" },
};

export default function PromptItem({ prompt }: PromptItemProps) {
  const normalizedStatus = normalizePromptStatus(prompt.status);
  const meta = statusMeta[normalizedStatus];

  return (
    <article className="prompt-card">
      <div className="prompt-card__header">
        <div>
          <p className="panel-kicker">Prompt</p>
          <h3>{prompt.prompt}</h3>
        </div>
        <span className={`status-pill ${meta.tone}`}>{meta.label}</span>
      </div>

      {prompt.response ? (
        <div className="response-block">
          <span>Response</span>
          <p>{prompt.response}</p>
        </div>
      ) : null}

      {prompt.createdAt ? (
        <p className="meta-text">
          Created: {new Date(prompt.createdAt).toLocaleString("en-GB")}
        </p>
      ) : null}
    </article>
  );
}
