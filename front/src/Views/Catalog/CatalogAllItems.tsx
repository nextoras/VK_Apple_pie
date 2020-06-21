import * as React from 'react';

import { Div, Panel } from "@vkontakte/vkui";

import ItemCard from "./ItemCard";

interface CatalogAllItemsProps {
    id: string;
    setView: Function;
    cards: any[];
    user: any;
}

const CatalogAllItems = (props) => {
    return (
        <Panel id={props.id}>
            {
                props.map(item => {
                    <ItemCard {...item} />
                })
            }
        </Panel>
    )
}

export default CatalogAllItems;
