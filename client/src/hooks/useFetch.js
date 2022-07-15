import { useEffect, useState } from "react";

export function FetchHelper(action) {
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);
  const [status, setStatus] = useState(null);
  useEffect(() => {
    (async function () {
      try {
        setLoading(true);
        const response = await action();
        setStatus(response.status);
        setData(response.data);
      } catch (err) {
        setError(err);
      } finally {
        setLoading(false);
      }
    })();
  }, [action]);

  return { data, status, error, loading };
}
