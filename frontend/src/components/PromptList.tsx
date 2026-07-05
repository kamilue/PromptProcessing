import type { PromptListProps } from "../types";
import PromptItem from "./PromptItem";

export default function PromptList({ prompts }: PromptListProps) {
  if (prompts.length === 0) {
    return (
      <div className="empty-state">
        <h3>No prompts yet</h3>
        <p>Add your first prompt to start the processing flow.</p>
      </div>
    );
  }

  return (
    <div className="prompt-list">
      {prompts.map((prompt) => (
        <PromptItem key={prompt.id} prompt={prompt} />
      ))}
    </div>
  );
}
