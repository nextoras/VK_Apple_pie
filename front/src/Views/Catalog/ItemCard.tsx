import * as React from 'react';

import {Div, Text, Title} from '@vkontakte/vkui';

import './Catalog.css';

interface ItemCardProps {
    price: string;
    description: string;
    image: any
}

const ItemCard = (props: ItemCardProps) => {
    return (
        <div className="itemCard">
            <img src={props.image} className="itemCardImage" />
            <Title weight="regular" level="3" />
            <Text weight="regular">
                {props.description}
            </Text>
        </div>
    )
}

export default ItemCard;
