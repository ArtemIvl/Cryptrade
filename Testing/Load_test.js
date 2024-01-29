import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    stages: [
        { duration: '5s', target: 10 }, // scale up to 10 users over 5s
        { duration: '30s', target: 10 }, // stay at 10 users for 30s
        { duration: '5s', target: 20 }, // scale up to 20 users over 5s
        { duration: '30s', target: 20 }, // stay at 20 users for 30s
        { duration: '5s', target: 30 }, // scale up to 30 users over 5s
        { duration: '30s', target: 30 }, // stay at 30 users for 30s
        { duration: '5s', target: 40 }, // scale up to 40 users over 5s
        { duration: '30s', target: 40 }, // stay at 40 users for 30s
        { duration: '10s', target: 0 }, // scale down to 0 users over 10s
    ],
    // thresholds: {
    //     http_req_duration: ['p(95)<500'], // 95% of requests must complete below 500ms
    // },
};
export default () => {
    http.get('https://localhost:8005/api/Crypto');
    sleep(1);
};