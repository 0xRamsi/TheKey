import React, { useContext, useEffect, useState } from 'react'
import { PortContext } from './context/PortContext.js';
import BlogpostList from './components/BlogpostList';

export default function App({ port }) {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        setLoading(true);
        fetch('/TheKeyAcademy/GetBlogData')
        .then(response => response.json())
        .then(blogEntries => {
            setLoading(false);
            setPosts(blogEntries);
        })
    },
    []);

    return (<>
        <h4>Word Count - React</h4>
        {(loading) ?
            (<h3>Loading...</h3>)
            :
            <PortContext.Provider value={port}>
                <BlogpostList posts={posts} />
            </PortContext.Provider>
        }
    </>)
}