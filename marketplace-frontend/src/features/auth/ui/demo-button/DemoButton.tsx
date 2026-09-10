import styles from "./DemoButton.module.css";
import {Button} from "@/shared/ui/button";
import {Popover} from "@/shared/ui/popover";
import {useEffect, useRef, useState} from "react";


interface DemoButtonProps {
    onClick?: () => void;
}

export const DemoButton = (
    {
        onClick,
    }: DemoButtonProps
) => {

    const [isHovering, setHovering] = useState(false);
    const anchorRef = useRef<HTMLButtonElement>(null);

    useEffect(() => {

    });

    return (
        <>
            <Button
                ref={anchorRef}
                variant="primary"
                size="large"
                fullWidth
                onClick={() => onClick?.()}
                onMouseEnter={() => {
                    setHovering(true)
                }}
                onMouseLeave={() => {
                    setHovering(false)
                }}>
                Демо-вход
            </Button>

            <Popover
                className={styles.popover}
                isOpen={isHovering}
                anchorRef={anchorRef}
                placement={"right-start"}
            >
                <p className={styles.header}>Внимание!</p>

                <div className={styles.info}>
                    <p className={[styles.text, styles.textBold].join(" ")}>
                        Для демо-входа введите корректную почту
                    </p>

                    <p className={styles.text}>
                        Отправить код можно только на почту artrad32@gmail.com
                    </p>
                </div>


            </Popover>
        </>
    );
}