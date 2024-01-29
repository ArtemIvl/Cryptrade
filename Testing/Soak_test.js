import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 35 }, // ramp up to 35 users over 10s
        { duration: '10m', target: 35 }, // stay at 35 users for 10m
        { duration: '10s', target: 0 }, // ramp down to 0 users over 10s
    ],
};
export default () => {
    http.get('https://localhost:8005/api/Crypto');
    sleep(1);
};
