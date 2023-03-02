import React from 'react'
import Blogpost from './Blogpost'

export default function BlogpostList({ posts }){
    if(!posts?.length){
        return <h3>No posts to show</h3>;
    }
    return posts.map(post => <Blogpost {...post} />)
}