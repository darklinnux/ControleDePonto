import { useState } from "react";

export function useLoading() {
  const [isLoading, setIsLoading] = useState(true);

  return { isLoading, setIsLoading }
}
