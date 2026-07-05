import { useState, type FormEvent } from "react";
import { createPrompt } from "../api/promptsApi";
import type { PromptFormProps } from "../types";

export default function PromptForm({ onCreated }: PromptFormProps) {
  const [text, setText] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [feedback, setFeedback] = useState<{
    type: "success" | "error";
    message: string;
  } | null>(null);

  const submit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const value = text.trim();
    if (!value) {
      setFeedback({
        type: "error",
        message: "Please enter a prompt before sending it.",
      });
      return;
    }

    try {
      setIsSubmitting(true);
      setFeedback(null);
      await createPrompt(value);
      setText("");
      setFeedback({
        type: "success",
        message: "Prompt was added successfully.",
      });
      onCreated();
    } catch {
      setFeedback({
        type: "error",
        message: "Could not add the prompt. Please check the backend.",
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <section className="panel panel-form">
      <div className="panel-header">
        <div>
          <p className="panel-kicker">New prompt</p>
          <h2>Add a prompt for processing</h2>
        </div>
      </div>

      <form onSubmit={submit} className="form-stack">
        <label className="field-label" htmlFor="prompt-text">
          Prompt content
        </label>
        <textarea
          id="prompt-text"
          value={text}
          onChange={(event) => setText(event.target.value)}
          placeholder="e.g. Create a launch plan for a new product..."
          rows={5}
          className="prompt-input"
        />

        <button
          type="submit"
          className="primary-button"
          disabled={isSubmitting}
        >
          {isSubmitting ? "Sending..." : "Send prompt"}
        </button>

        {feedback ? (
          <p className={`feedback ${feedback.type}`}>{feedback.message}</p>
        ) : null}
      </form>
    </section>
  );
}
