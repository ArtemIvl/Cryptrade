import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    stages: [
        { duration: '5s', target: 10 }, // scale up to 10 users over 5s
        { duration: '30s', target: 10 }, // stay at 10 users for 30s
        { duration: '5s', target: 200 }, // scale up to 200 users over 5s
        { duration: '20s', target: 200 }, // stay at 200 users for 20s
        { duration: '10s', target: 0 }, // scale down to 0 users over 10s
    ],
};
export default () => {
    http.get('https://localhost:8005/api/Crypto');
    sleep(1);
};
