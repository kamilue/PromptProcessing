export default function PromptItem({ prompt }) {
  const getColor = (status) => {
    switch (status) {
      case 0:
        return "gray"; // Pending
      case 1:
        return "orange"; // Processing
      case 2:
        return "green"; // Completed
      case 3:
        return "red"; // Failed
      default:
        return "black";
    }
  };

  return (
    <div
      style={{
        border: "1px solid #ddd",
        marginBottom: 10,
        padding: 10,
      }}
    >
      <div>
        <b>Prompt:</b> {prompt.prompt}
      </div>

      <div style={{ color: getColor(prompt.status) }}>
        Status: {prompt.status}
      </div>

      {prompt.response && (
        <div style={{ marginTop: 10 }}>
          <b>Response:</b> {prompt.response}
        </div>
      )}
    </div>
  );
}
