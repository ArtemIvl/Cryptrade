import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
    stages: [
        { duration: '5s', target: 5 }, // ramp up to 5 users over 5s
        { duration: '30s', target: 5 }, // stay at 5 users for 30s
        { duration: '5s', target: 40 }, // ramp up to 40 users over 5s
        { duration: '30s', target: 40 }, // stay at 40 users for 30s
        { duration: '5s', target: 150 }, // ramp up to 150 users over 5s
        { duration: '30s', target: 150 }, // stay at 150 users for 30s
        { duration: '5s', target: 250 }, // ramp up to 250 users over 5s 
        { duration: '30s', target: 250 }, // stay at 250 users for 30s
        { duration: '5s', target: 0 }, // ramp down to 0 users over 5s
    ],
};
export default () => {
    http.get('https://localhost:8005/api/Crypto');
    sleep(1);
};