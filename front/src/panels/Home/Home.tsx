import * as React from 'react';
import Panel from '@vkontakte/vkui/dist/components/Panel/Panel';
import PanelHeader from '@vkontakte/vkui/dist/components/PanelHeader/PanelHeader';
import Button from '@vkontakte/vkui/dist/components/Button/Button';
import Group from '@vkontakte/vkui/dist/components/Group/Group';
import Cell from '@vkontakte/vkui/dist/components/Cell/Cell';
import Div from '@vkontakte/vkui/dist/components/Div/Div';
import Avatar from '@vkontakte/vkui/dist/components/Avatar/Avatar';

interface HomeProps {
    id: string;
    go: Function;
    fetchedUser: any;
    photo_200: string;
    first_name: string;
    last_name: string;
    city: any;
}

const Home = ({ id, go, fetchedUser }: HomeProps) => (
    <Panel id={id}>
        <PanelHeader>Example</PanelHeader>
        {fetchedUser && (
            <Group title="User Data Fetched with VK Bridge">
                <Cell
                    before={
                        fetchedUser.photo_200 ? (
                            <Avatar src={fetchedUser.photo_200} />
                        ) : null
                    }
                    description={
                        fetchedUser.city && fetchedUser.city.title
                            ? fetchedUser.city.title
                            : ''
                    }
                >
                    {`${fetchedUser.first_name} ${fetchedUser.last_name}`}
                </Cell>
            </Group>
        )}

        <Group title="Navigation Example">
            <Div>
                <Button size="xl" level="2" onClick={go} data-to="persik">
                    Show me the Persik, please
                </Button>
            </Div>
        </Group>
    </Panel>
);

export default Home;
