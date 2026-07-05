export type PromptStatus = 0 | 1 | 2 | 3;

export interface PromptItemData {
  id: number;
  prompt: string;
  status?: PromptStatus | string | null;
  response?: string;
  createdAt?: string;
}

export interface PromptFormProps {
  onCreated: () => void;
}

export interface PromptListProps {
  prompts: PromptItemData[];
}

export interface PromptItemProps {
  prompt: PromptItemData;
}

export function normalizePromptStatus(
  status: PromptItemData["status"],
): PromptStatus {
  if (typeof status === "number") {
    return status >= 0 && status <= 3 ? (status as PromptStatus) : 0;
  }

  if (typeof status === "string") {
    const normalized = status.trim().toLowerCase();

    const mapping: Record<string, PromptStatus> = {
      pending: 0,
      queued: 0,
      processing: 1,
      "in-progress": 1,
      inprogress: 1,
      completed: 2,
      done: 2,
      success: 2,
      failed: 3,
      error: 3,
    };

    if (mapping[normalized] !== undefined) {
      return mapping[normalized];
    }

    const numeric = Number(normalized);
    if (Number.isInteger(numeric) && numeric >= 0 && numeric <= 3) {
      return numeric as PromptStatus;
    }
  }

  return 0;
}
