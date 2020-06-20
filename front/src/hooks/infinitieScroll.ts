import { useState, useEffect } from 'react';

const useInfiniteScroll = callback => {
    const [isFetching, setIsFetching] = useState(false);

    useEffect(() => {
        window.addEventListener('scroll', handleScroll);

        return () => window.removeEventListener('scroll', handleScroll);
    }, []);

    useEffect(() => {
        if (!isFetching) return;
        callback();
    }, [isFetching]);

    function handleScroll(event) {
        const observer = new IntersectionObserver(
            (entries, observer) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        setIsFetching(true);

                        console.log(entry.target);
                        observer.unobserve(entry.target);
                    }
                });
            },
            {
                root: null,
                rootMargin: '0px',
                threshold: 0,
            },
        );

        // #TODO: Избавиться от констант
        observer.observe(document.getElementById('cardsWrapper').querySelector('#last'));
    }

    return [isFetching, setIsFetching];
};

export default useInfiniteScroll;
