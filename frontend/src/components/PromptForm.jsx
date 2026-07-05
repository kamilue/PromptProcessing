import { useState } from "react";
import { createPrompt } from "../api/promptsApi";

export default function PromptForm({ onCreated }) {
  const [text, setText] = useState("");

  const submit = async () => {
    if (!text.trim()) return;

    try {
      await createPrompt(text);

      setText("");

      onCreated();
    } catch (e) {
      alert(e, "Backend is not available.");
    }
  };

  return (
    <div style={{ marginBottom: 20 }}>
      <input
        value={text}
        onChange={(e) => setText(e.target.value)}
        placeholder="Enter prompt..."
        style={{ width: 300, padding: 8 }}
      />
      <button onClick={submit} style={{ marginLeft: 10 }}>
        Send
      </button>
    </div>
  );
}
