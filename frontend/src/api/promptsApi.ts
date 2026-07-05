import axios from "axios";
import type { PromptItemData } from "../types";

const API = "http://localhost:7267/api/prompts";

export const getPrompts = async (): Promise<PromptItemData[]> => {
  try {
    const res = await axios.get<PromptItemData[]>(API);
    return res.data;
  } catch {
    return [];
  }
};

export const createPrompt = async (prompt: string): Promise<PromptItemData> => {
  const res = await axios.post<PromptItemData>(API, { prompt });
  return res.data;
};
