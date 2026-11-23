export async function onRequestGet(context) {
  const url = new URL(context.request.url);
  const contestId = url.searchParams.get("contestId") || "1";

  const targetUrl = `https://dessert-scores.gtfullton.workers.dev/api/results?contestId=${encodeURIComponent(contestId)}`;  const response = await fetch(targetUrl, { headers: { "Authorization": `Bearer ${context.env.API_TOKEN}`,
      "Accept": "application/json"
    }
  });

  const data = await response.text();

  return new Response(data, {
    status: response.status,
    headers: {
      "Content-Type": "application/json"
    }
  });
}