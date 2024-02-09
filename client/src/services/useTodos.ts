import { useSuspenseQuery } from '@tanstack/react-query';
import { getTodosQueryKey } from './constants';
import { getAuthHeaders } from './getAuthHeaders';
import axios from 'axios';

const dummyData = {
    data: [
        {
            id: '1',
            name: 'Bribe my plants into not wilting',
            checked: false,
        },
        {
            id: '2',
            name: 'Pretend to adult for a few hours',
            checked: true,
        },
        {
            id: '3',
            name: 'Embark on a sock quest',
            checked: false,
        },
    ]
};

axios.interceptors.response.use(
    function (response) {
        return response;
    }, 
    function () {
        return dummyData;
    }
);

export const useTodos = () => {
    return useSuspenseQuery(({
        queryKey: getTodosQueryKey.queryKey!,
        queryFn: async () => {
            const response = await axios.get('/api/todo', {
                headers: { ...getAuthHeaders()}
            });

            return response.data;
        }
    }));
};