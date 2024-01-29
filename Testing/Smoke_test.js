import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    vus: 1, // 1 user looping for 1 minute
    duration: '1m',
    thresholds: {
        http_req_duration: ['p(95)<1000'] // 95% of requests must complete below 1s
    },
};
export default function () {
    http.get('https://localhost:8005/api/Crypto');
    sleep(1);
}