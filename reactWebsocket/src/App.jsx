import React, { useEffect, useState } from 'react'
import BlogpostList from './components/BlogpostList';

export default function App() {
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        setLoading(true);
        // const ws = new WebSocket("wss://localhost:49201/ws");
        // setWs(ws);
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
            : <BlogpostList posts={posts} />
        }
    </>)
}