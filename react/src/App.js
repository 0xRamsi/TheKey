import React, { useEffect, useState } from 'react'
import BlogpostList from './components/BlogpostList';

export default function App() {
    const [posts, setPosts] = useState([{
        title:'My story',
        authorName: 'ramzi',
        date: Date.now(),
        link: '',
    }]);


    useEffect(() => {
        fetch('/TheKeyAcademy/GetBlogData')
        .then(response => response.json())
        .then(blogEntries => {
            console.log(blogEntries);
            setPosts(blogEntries);
        })
    },
    []);


    return (<>
    <h4>Hallt, Welt!</h4>
    <BlogpostList posts={posts} />
    </>)
}