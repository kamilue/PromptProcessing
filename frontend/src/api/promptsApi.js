import axios from "axios";

const API = "http://localhost:7267/api/prompts";

export const getPrompts = async () => {
  try {
    const res = await axios.get(API);
    return res.data;
  } catch {
    return [];
  }
};

export const createPrompt = async (prompt) => {
  const res = await axios.post(API, { prompt });
  return res.data;
};
