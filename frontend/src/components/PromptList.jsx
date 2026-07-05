import PromptItem from "./PromptItem";

export default function PromptList({ prompts }) {
  return (
    <div>
      {prompts.map((p) => (
        <PromptItem key={p.id} prompt={p} />
      ))}
    </div>
  );
}
